﻿/**************************************************************************
 *                                                                        *
 *  File:        Model.cs                                                 *
 *  Copyright:   (c) 2021, Baltariu Ionut-Alexandru                       *
 *  E-mail:      ionut-alexandru.baltariu@student.tuiasi.ro               *
 *  Description: The model class of the Model-View-Presenter architecture.*
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


namespace Model
{
    using Memento;
    using Shared;
    using Strategy;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Model class - contains the data, state and logic of the application.
    /// </summary>
    public class Model : IModel
    {
        #region Private Fields
        private string _saveFileName;
        private UndoStack _undoStack;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the model - initializes the undo stack
        /// </summary>
        public Model()
        {
            _undoStack = new UndoStack();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the strategy for a painting tool
        /// </summary>
        /// <param name="paintingTool">The given painting tool</param>
        /// <returns>The strategy corresponding to the painting tool</returns>
        public Strategy GetPaintingStrategy(PaintingTool paintingTool)
        {
            Strategy paintingStrategy = null;

            switch (paintingTool)
            {
                case PaintingTool.Brush:
                    paintingStrategy = new BrushStrategy();
                    break;
                case PaintingTool.Line:
                    paintingStrategy = new LineStrategy();
                    break;
                case PaintingTool.Circle:
                    paintingStrategy = new CircleStrategy();
                    break;
                case PaintingTool.Ellipse:
                    paintingStrategy = new ElipseStrategy();
                    break;
                case PaintingTool.Square:
                    paintingStrategy = new SquareStrategy();
                    break;
                case PaintingTool.Rectangle:
                    paintingStrategy = new RectangleStrategy();
                    break;
                case PaintingTool.Star:
                    paintingStrategy = new StarStrategy();
                    break;
                case PaintingTool.RightTriangle:
                    paintingStrategy = new RightTriangleStrategy();
                    break;
                case PaintingTool.IsoscelesTriangle:
                    paintingStrategy = new IsoscelesTriangleStrategy();
                    break;
                case PaintingTool.Rhomb:
                    paintingStrategy = new RhombStrategy();
                    break;
                case PaintingTool.Pentagon:
                    paintingStrategy = new PentagonStrategy();
                    break;
                case PaintingTool.Hexagon:
                    paintingStrategy = new HexagonStrategy();
                    break;
                case PaintingTool.ArrowLeft:
                    paintingStrategy = new ArrowLeftStrategy();
                    break;
                case PaintingTool.ArrowRight:
                    paintingStrategy = new ArrowRightStrategy();
                    break;
                case PaintingTool.ArrowUp:
                    paintingStrategy = new ArrowUpStrategy();
                    break;
                case PaintingTool.ArrowDown:
                    paintingStrategy = new ArrowDownStrategy();
                    break;
                case PaintingTool.Heart:
                    paintingStrategy = new HeartStrategy();
                    break;
                case PaintingTool.Eraser:
                    paintingStrategy = new EraserStrategy();
                    break;
                    // no default case is required as possible values are found in an enum and cannot be given incorrectly
            }

            return paintingStrategy;
        }

        /// <summary>
        /// Checks if a file name has been given when saving the drawing.
        /// </summary>
        /// <returns>Boolean value of the stated scenario</returns>
        public bool HasSaveFileName()
        {
            bool hasSaveFileName = false;

            if (_saveFileName != null)
            {
                hasSaveFileName = true;
            }

            return hasSaveFileName;
        }

        /// <summary>
        /// Loads a drawing from the given path
        /// </summary>
        /// <param name="filename">Drawing path on disk</param>
        /// <returns>The drawing at the given path</returns>
        public DrawingMemento LoadDrawing(string filename)
        {
            DrawingMemento loadedDrawing = null;

            try
            {
                using (var temporaryBmp = new Bitmap(filename))
                {
                    Image loadedImageCopy = new Bitmap(temporaryBmp);
                    loadedDrawing = new DrawingMemento(loadedImageCopy, "Loaded " + filename);
                }
            }
            catch (ArgumentException argumentException)
            {
                throw new Exception("Error: Invalid path -> " + argumentException.Message);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw new Exception("Error: File not found to load drawing -> " + fileNotFoundException.Message);
            }
            catch (OutOfMemoryException outOfMemoryException)
            {
                throw new Exception("Error: Out of memory-> " + outOfMemoryException.Message);
            }

            return loadedDrawing;
        }

        /// <summary>
        /// Redoes the last undone action
        /// </summary>
        /// <returns>The drawing with the corresponding content</returns>
        public DrawingMemento Redo()
        {
            return _undoStack.Redo();
        }

        /// <summary>
        /// Saves a drawing at the path mentioned in _saveFileName
        /// </summary>
        /// <param name="drawingMemento">The drawing</param>
        /// <returns>void</returns>
        public void SaveDrawing(DrawingMemento drawingMemento)
        {

            if (HasSaveFileName() == true)
            {
                ImageFormat drawingFormat = ImageFormat.Png;
                string drawingExtension = string.Empty;

                try
                {
                    drawingExtension = Path.GetExtension(_saveFileName);
                }
                catch (ArgumentException argumentException)
                {
                    throw new Exception("Error: Invalid path -> " + argumentException.Message);
                }

                switch (drawingExtension)
                {
                    case ".png":
                        drawingFormat = ImageFormat.Png;
                        break;
                    case ".bmp":
                        drawingFormat = ImageFormat.Bmp;
                        break;
                }

                drawingMemento.Drawing.Save(_saveFileName, drawingFormat);
                DropMementos();
                AddMemento(drawingMemento);
            }
        }

        /// <summary>
        /// Setter for the drawing file save path
        /// </summary>
        /// <param name="saveFileName">The wished path</param>
        /// <returns>void</returns>
        public void SetSaveFileName(string saveFileName)
        {
            _saveFileName = saveFileName;
        }

        /// <summary>
        /// Undoes the last done action
        /// </summary>
        /// <returns>The drawing with the corresponding content</returns>
        public DrawingMemento Undo()
        {
            return _undoStack.Undo();
        }


        /// <summary>
        /// Function used to add a drawing memento to the event stack
        /// </summary>
        /// <param name="memento">The memento to be added on the stack</param>
        /// <returns>void</returns>
        public void AddMemento(DrawingMemento memento)
        {
            _undoStack.Add(memento);
        }

        /// <summary>
        /// Used to get the description of the next undo that's on the stack
        /// </summary>
        /// <returns>The description of the next undo on the stack</returns>
        public string GetNextUndoDescription()
        {
            return _undoStack.GetNextUndoDescription();
        }

        /// <summary>
        /// Used to get the description of the next redo that's on the stack
        /// </summary>
        /// <returns>The description of the next redo on the stack</returns>
        public string GetNextRedoDescription()
        {
            return _undoStack.GetNextRedoDescription();
        }

        /// <summary>
        /// Wrapper for the undo stack drop(clear) function
        /// </summary>
        /// <returns>void</returns>
        public void DropMementos()
        {
            _undoStack.Drop();
        }
        #endregion
    }

}

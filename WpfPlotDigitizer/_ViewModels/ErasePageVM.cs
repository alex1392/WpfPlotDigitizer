﻿using CycWpfLibrary;
using CycWpfLibrary.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfPlotDigitizer.DI;
using System.Windows.Input;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Media.Imaging;
using CycWpfLibrary.Emgu;

namespace WpfPlotDigitizer
{
  public class ErasePageVM
  {

    public ErasePageVM()
    {
      UndoCommand = editor.UndoCommand;
      RedoCommand = editor.RedoCommand;
      
    }

    private Image<Bgra, byte> imageDisplay;
    public BitmapSource imageSource => imageDisplay?.ToBitmapSource();
    public EditorBase editor { get; private set; } = new ObjectEditor();

    public ICommand UndoCommand { get; private set; }
    public ICommand RedoCommand { get; private set; }

  }
}

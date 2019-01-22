﻿using CycWpfLibrary.Controls;
using CycWpfLibrary.Media;
using CycWpfLibrary.MVVM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static WpfPlotDigitizer.DI;

namespace WpfPlotDigitizer
{
  public class BrowsePageVM : ViewModelBase
  {
    public BrowsePageVM()
    {
      OpenFileCommand = new RelayCommand(OpenFile);
    }

    private string fileName;
    public string FileName
    {
      get => fileName;
      set
      {
        if (ImageExts.List.Contains(Path.GetExtension(value)))
        {
          fileName = value;
          PBInput = new Bitmap(fileName).ToPixelBitmap();
        }
        else
        {
          MessageBox.Show("Input file should be an image.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
      }
    }

    public PixelBitmap PBInput
    {
      get => imageData?.PBInput;
      set
      {
        imageData.PBInput = value;
        appManager.PageManager.TurnNext(); //設定好就直接翻頁
      }
    }

    public ICommand OpenFileCommand { get; set; }
    public void OpenFile()
    {
      var dialog = new OpenFileDialog();
      var imageExtensions = ImageExts.String;
      dialog.Filter = "Images|" + imageExtensions + "|All|*.*";
      if (dialog.ShowDialog() == false)
      {
        return;
      }
      PBInput = new BitmapImage(new Uri(dialog.FileName)).ToPixelBitmap();
    }
  }
}

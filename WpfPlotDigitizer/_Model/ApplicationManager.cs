﻿using CycWpfLibrary;
using CycWpfLibrary.Controls;
using CycWpfLibrary.Emgu;
using CycWpfLibrary.Media;
using CycWpfLibrary.MVVM;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using System.Threading.Tasks;
using static CycWpfLibrary.NativeMethod;
using static WpfPlotDigitizer.DI;

namespace WpfPlotDigitizer
{
  public class ApplicationManager
  {
    public ApplicationManager()
    {
      PageManager.TurnNextEvent += OnTurnNext;
    }

    public PageManagerBase PageManager { get; private set; } = new PageManager();

    /// <summary>
    /// Called whenever <see cref="PageManager.TurnNext"/> is fired.
    /// </summary>
    private void OnTurnNext()
    {
      // call before actually turned next
      switch ((ApplicationPages)PageManager.Index + 1)
      {
        case ApplicationPages.Axis:
          imageData.PBFilterW = new PixelBitmap(imageData.PBInput.Size)
          {
            Pixel = ImageProcessing.FilterW(imageData.PBInput)
          };
          axisPageVM.GetAxis();
          break;
        case ApplicationPages.AxisLimit:
          imageData.PBAxis = imageData.PBInput.Bitmap
                                        .Crop(imageData.Axis)
                                        .ToPixelBitmap();
          axLimPageVM.GetAxisLimit();
          break;
        case ApplicationPages.Filter:
          imageData.ImageAxis = imageData.PBAxis.ToImage<Bgra, byte>();
          imageData.ImageFilterRGB = imageData.ImageAxis.Clone();
          filterPageVM.InRange();
          break;
        case ApplicationPages.Erase:
          imageData.ImageErase = imageData.ImageFilterRGB.Clone();
          erasePageVM.editManager.Init(imageData.ImageErase);
          break;
        case ApplicationPages.Size:
          imageData.ImageErase = erasePageVM.editManager.Object as Image<Bgra, byte>;
          sizePageVM.imageDisplay = imageData.ImageErase.Clone();
          sizePageVM.ParamChanged();
          break;
        default:
          break;
      }
    }
  }
}
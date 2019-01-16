﻿using System.Windows.Controls;
using static WpfPlotDigitizer.Singletons;

namespace WpfPlotDigitizer
{
  /// <summary>
  /// FilterPage.xaml 的互動邏輯
  /// </summary>
  public partial class FilterPage : UserControl
  {
    public FilterPage()
    {
      InitializeComponent();

      DataContext = filterPageVM;
    }
  }
}

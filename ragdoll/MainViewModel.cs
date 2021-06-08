using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;

namespace ragdoll
{
  public partial class MainViewModel : BindableBase
  {
    private AppContext _appContext = new ();
    private ThicknessConverter _thicknessConverter = new ();
    private BrushConverter _brushConverter = new ();

    public MainViewModel()
    {
      this.WindowTitle = "ragdoll";
      this.ResizeBorderThickness = 5;
      this.CaptionHeight = 30;

      this.WindowHeight = 450;
      this.WindowWidth = 800;
      this.WindowMarginWidth = 12;
      this.ForegroundColor = Brushes.White;
      this.BackgroundColor = (Brush)_brushConverter.ConvertFromString("#cc222222");
      this.SubForegroundColor = Brushes.Gray;
      this.PinIconColor = Brushes.White;

      this.Title__pinned = "ピン止め済みのウィンドウ";
      this.Title__not_pinned = "ピン止めされていないウィンドウ";
      this.TitleFontSize = 18;
      this.TitleMargin = (Thickness)_thicknessConverter.ConvertFromString($"{this.WindowMarginWidth}, 0, 0, 0");
      this.MainFontSize = 15;
      this.ItemMarginWidth = AppContext.ItemMarginWidth;
      this.ContentSpacing = 30;
      this.ContentSpacingMargin = (Thickness)_thicknessConverter.ConvertFromString($"24, {(this.ContentSpacing - 2) / 2}");
      this.IndentWidth = this.WindowMarginWidth * 2;
      this.ItemIndent = (Thickness)_thicknessConverter.ConvertFromString($"{this.IndentWidth}, 0, 0, 0");
      this.PinIconWidth = 48;
      this.ItemTextMarginWidth = 3;
      this.ItemTextMargin = (Thickness)_thicknessConverter.ConvertFromString($"{AppContext.ItemMarginWidth}, 0, 0, 0");

      this.CloseButtonWidth = 60;
      this.CloseButtonFontSize = 14;
      this.CloseButtonRightMargin = 0;
      this.CloseButtonMargin = (Thickness)_thicknessConverter.ConvertFromString($"0, 0, {this.CloseButtonRightMargin}, 0");

      this.WindowInformations__pinned = new ();
      this.WindowInformations__not_pinned = new ();

      this.GetMainWindowCommand = new DelegateCommand(ExecuteGetMainWindow, CanExecuteGetMainWindow);
      this.PinWindowCommand = new DelegateCommand(ExecutePinWindow, CanExecutePinWindow);
      this.UnPinWindowCommand = new DelegateCommand(ExecuteUnPinWindow, CanExecuteUnPinWindow);
      this.CloseWindowCommand = new DelegateCommand(ExecuteCloseWindow, CanExecuteCloseWindow);
    }

    private string _windowTitle;
    public string WindowTitle
    {
      get { return this._windowTitle; }
      set { this.SetProperty(ref this._windowTitle, value); }
    }

    private int _resizeBorderThickness;
    public int ResizeBorderThickness
    {
      get { return this._resizeBorderThickness; }
      set { this.SetProperty(ref this._resizeBorderThickness, value); }
    }

    private int _captionHeight;
    public int CaptionHeight
    {
      get { return this._captionHeight; }
      set { this.SetProperty(ref this._captionHeight, value); }
    }

    private int _windowHeight;
    public int WindowHeight
    {
      get { return this._windowHeight; }
      set { this.SetProperty(ref this._windowHeight, value); }
    }

    private int _windowWidth;
    public int WindowWidth
    {
      get { return this._windowWidth; }
      set { this.SetProperty(ref this._windowWidth, value); }
    }

    private int _windowMarginWidth;
    public int WindowMarginWidth
    {
      get { return this._windowMarginWidth; }
      set { this.SetProperty(ref this._windowMarginWidth, value); }
    }

    private Brush _foregroundColor;
    public Brush ForegroundColor
    {
      get { return this._foregroundColor; }
      set { this.SetProperty(ref this._foregroundColor, value); }
    }

    private Brush _backgroundColor;
    public Brush BackgroundColor
    {
      get { return this._backgroundColor; }
      set { this.SetProperty(ref this._backgroundColor, value); }
    }

    private Brush _subforegroundColor;
    public Brush SubForegroundColor
    {
      get { return this._subforegroundColor; }
      set { this.SetProperty(ref this._subforegroundColor, value); }
    }

    private Brush _pinIconColor;
    public Brush PinIconColor
    {
      get { return this._pinIconColor; }
      set { this.SetProperty(ref this._pinIconColor, value); }
    }

    private string _title__pinned;
    public string Title__pinned
    {
      get { return this._title__pinned; }
      set { this.SetProperty(ref this._title__pinned, value); }
    }

    private string _title__not_pinned;
    public string Title__not_pinned
    {
      get { return this._title__not_pinned; }
      set { this.SetProperty(ref this._title__not_pinned, value); }
    }

    private int _titleFontSize;
    public int TitleFontSize
    {
      get { return this._titleFontSize; }
      set { this.SetProperty(ref this._titleFontSize, value); }
    }

    private Thickness _titleMargin;
    public Thickness TitleMargin
    {
      get { return this._titleMargin; }
      set { this.SetProperty(ref this._titleMargin, value); }
    }

    private int _mainFontSize;
    public int MainFontSize
    {
      get { return this._mainFontSize; }
      set { this.SetProperty(ref this._mainFontSize, value); }
    }

    private int _itemMarginWidth;
    public int ItemMarginWidth
    {
      get { return this._itemMarginWidth; }
      set { this.SetProperty(ref this._itemMarginWidth, value); }
    }

    private int _contentSpacing;
    public int ContentSpacing
    {
      get { return this._contentSpacing; }
      set { this.SetProperty(ref this._contentSpacing, value); }
    }

    private Thickness _contentSpacingMargin;
    public Thickness ContentSpacingMargin
    {
      get { return this._contentSpacingMargin; }
      set { this.SetProperty(ref this._contentSpacingMargin, value); }
    }

    private Thickness _itemMargin;
    public Thickness ItemMargin
    {
      get { return this._itemMargin; }
      set { this.SetProperty(ref this._itemMargin, value); }
    }

    private int _indentWidth;
    public int IndentWidth
    {
      get { return this._indentWidth; }
      set { this.SetProperty(ref this._indentWidth, value); }
    }

    private Thickness _itemIndent;
    public Thickness ItemIndent
    {
      get { return this._itemIndent; }
      set { this.SetProperty(ref this._itemIndent, value); }
    }

    private int _pinIconWidth;
    public int PinIconWidth
    {
      get { return this._pinIconWidth; }
      set { this.SetProperty(ref this._pinIconWidth, value); }
    }

    private int _itemTextMarginWidth;
    public int ItemTextMarginWidth
    {
      get { return this._itemTextMarginWidth; }
      set { this.SetProperty(ref this._itemTextMarginWidth, value); }
    }

    private Thickness _itemTextMargin;
    public Thickness ItemTextMargin
    {
      get { return this._itemTextMargin; }
      set { this.SetProperty(ref this._itemTextMargin, value); }
    }

    private int _closeButtonWidth;
    public int CloseButtonWidth
    {
      get { return this._closeButtonWidth; }
      set { this.SetProperty(ref this._closeButtonWidth, value); }
    }

    private int _closeButtonFontSize;
    public int CloseButtonFontSize
    {
      get { return this._closeButtonFontSize; }
      set { this.SetProperty(ref this._closeButtonFontSize, value); }
    }

    private int _closeButtonRightMargin;
    public int CloseButtonRightMargin
    {
      get { return this._closeButtonRightMargin; }
      set { this.SetProperty(ref this._closeButtonRightMargin, value); }
    }

    private Thickness _closeButtonMargin;
    public Thickness CloseButtonMargin
    {
      get { return this._closeButtonMargin; }
      set { this.SetProperty(ref this._closeButtonMargin, value); }
    }

    private ObservableCollection<WindowInformation> _windowInformations__pinned;
    public ObservableCollection<WindowInformation> WindowInformations__pinned
    {
      get { return this._windowInformations__pinned; }
      set { this.SetProperty(ref this._windowInformations__pinned, value); }
    }

    private ObservableCollection<WindowInformation> _windowInformations__not_pinned;
    public ObservableCollection<WindowInformation> WindowInformations__not_pinned
    {
      get { return this._windowInformations__not_pinned; }
      set { this.SetProperty(ref this._windowInformations__not_pinned, value); }
    }
  }
}

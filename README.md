# ConsoleUI.cs v0.1
A simple, extensible Console UI library in C-Sharp.

## Example
```csharp
 using Renderer;
 Label s = new Label("Hello, World!");
 Renderer.Render(s, new Vector2(5,5));
```
## Types:
```
Elements
    [x] Label
    [x] ProgressBar
    [X] RadioGroup
    [X] CheckBox
    [X] Panel
    [X] Button
    [x] Input Field
    [X] Int Input Field
    [X] OptionGroup
    [X] TextArea
    [X] ListView
    [X] Image
    [X] Vertical Scrollbar
    [X] Horizontal Scrollbar
    [X] GroupBox
    [X] BigTextBox
    [X] SelectListView
    [ ] TreeView
    [ ] Marquee
    [ ] Blink
    [X] Horizontal Rule
    [X] Slider
    [ ] Table
    [X] Pixel

Functionality
    [x] Renderer/IRenderable
    [X] Input (tab navigate) IInputable
    [X] ReRender
    [ ] Color
    [X] Derive from UIElement
    [ ] Animations
    [ ] OnHovers (bold, not bgcolor)
    [X] Placeholders in text boxes
    [ ] StyleInfo (new Button(text, styleInfo))
    [ ] TextArea selection (shift+arrows to select, copy/cut/paste)
    [X] TextBox/IntTextBox cursor

[ ] Make every property public, rerender on change
[ ] Ansi string class. IE new AnsiString("Hello") + " World!".Color(ConsoleColor.Green) + " Bold".Bold();
```
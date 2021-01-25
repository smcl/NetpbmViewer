# Feep

Quick-n-dirty viewer for .pbm, .pgm and .ppm files. Uses WPF and SkiaSharp with a pure .NET Netpbm interpreter of my own creation.

## TODO

- implementing the binary P4-6 formats
- comments in the P1-3 formats
- zoom functionality
- color picker to adjust the background in case of a white image
- align the displayed image better (left margin skinny, right margin huge)
- pipe image from stdin if no file arg provided
- show a nice simple ppm when there's no file produced, don't have my stupid hard-coded c:/Users/Sean/... file
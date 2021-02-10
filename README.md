# Feep

Quick-n-dirty viewer for .pbm, .pgm and .ppm files. Uses WPF and SkiaSharp with a pure .NET Netpbm interpreter of my own creation.

## TODO

- implementing the binary P4-6 formats
- comments in the P1-3 formats
- zoom functionality
- color picker to adjust the background in case of a white image
- align the displayed image better (left margin skinny, right margin huge)
- option to pipe image from stdin (`--` or `-stdin`?)
- when parse failure, show an error message ("failed to open xxx.ppm", etc)
- when parse failure, show a *useful* error message ("failed to open xxx.ppm, error on line 32, column 5, expected blah")
- filename in title
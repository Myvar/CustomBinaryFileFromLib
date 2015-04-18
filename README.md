# CustomBinaryFileFromLib
Custom Binary File From Lib (cbff) Is a library that allow you to easily create custom binary file formats in C# (or any .net laguage)

This package is available on NuGet Gallery.
To install Custom Binary File From Lib (cbff), run the following command in the Package Manager Console

```
PM> Install-Package CustomBinaryFileFromLib
```

# RoadMap
- Add suport for array file structures
- Add suport for sturcture type linking
- Create documnetaion

# Demo
here is a demo on how to use it (C#):
```
using CBFF;
// the paterns tell us the structure and layout of the file you can create your own
var FilePattern = @"
  file
  {
      header
      {
          id:string;
          isOpen:bool;
      }

      data:string;
  }";

  //create file
  var Wr = new CustomeBinaryFileWriter(FilePattern);
  //we set data using xpath's that is definded in file pattern
  Wr.SetValue("file.data", "test data");
  Wr.SetValue("file.header.id", "testfileid");
  Wr.SetValue("file.header.isOpen", true);
  Wr.WriteFile("test.bin");

  //read file
  var rd = new CustomBinaryFileReader(FilePattern, "test.bin");
  //we get data using xpath's that is definded in file pattern
  var filedata = rd.GetValue("file.data"); // returns "test data"(string)
  var fileid = rd.GetValue("file.header.id");// returns "testfileid"(string)
  var fileisopen = rd.GetValue("file.header.isOpen");//returns True (bool)
```
more detals on file patterns comming soon.

Base64encoder
-------------

This is a simple demonstration program to show how to sue the .NET API
to convert a file to and from base64 encoding.

I realise it's pretty simple, but someone asked for some help and I
thought it might be useful to others.

The program takes one and only one command-line parameter, the name of
a file to convert. This file can be binary or text, it shouldn't matter.

The program then:
* loads the file,
* converts it to base64 text,
* writes the base64 contents to a new file (the same name as the initial
  file, but with a '.base64' extension),
* reads the contents of this new file,
* converts it back from base64 to the original bytes,
* writes the newly-unconverted bytes to another new file (the same name
as the initial file, but with a '.decoded' extension),
* compares the original and decoded bytes to make sure they are identical.

It tries to output some useful information about what it's doing along the
way.
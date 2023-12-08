# Welcome to the GMP Native Interface for .NET Library

[![Test and publish](https://github.com/nethermindeth/math.gmp.native/actions/workflows/test-publish.yml/badge.svg)](https://github.com/nethermindeth/math.gmp.native/actions/workflows/test-publish.yml)
[![Nethermind.Gmp](https://img.shields.io/nuget/v/Nethermind.Gmp)](https://www.nuget.org/packages/Nethermind.Gmp)

The GMP Native Interface for .NET Library exposes to .NET (through P-Invoke and .NET types) all of
the functionality of the [GNU MP](https://gmplib.org/) Library (version 6.1.2).
It automatically loads at runtime the 32-bit or 64-bit GNU MP library that matches the current CPU
architecture, thus allowing building Visual Studio Projects for AnyCPU, x86, or x64.
It is based on the GNU MP "fat" build which automatically detects the current CPU type, and selects
any available assembly language code optimization for that CPU, thus providing optimal performance.

### Documentation

https://machinecognitis.github.io/Math.Gmp.Native

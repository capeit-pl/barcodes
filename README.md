# Code 128
## Type A
```csharp
var result = "Code 128".Code128EncodeTypeA();
var code128Stream = result.DrawPng(code128.Count, 100);
```
![code_128_encode_a](https://github.com/capeit-pl/barcodes/blob/main/sample/it_should_draw_code_128_encode_a.png?raw=true)

## Type B
```csharp
var result = "Code 128".Code128EncodeTypeB();
var code128Stream = result.DrawPng(code128.Count, 100);
```
![code_128_encode_b](https://github.com/capeit-pl/barcodes/blob/main/sample/it_should_draw_code_128_encode_b.png?raw=true)

## Type C
```csharp
var result = "Code 128".Code128EncodeTypeC();
var code128Stream = result.DrawPng(code128.Count, 100);
```
![code_128_encode_c](https://github.com/capeit-pl/barcodes/blob/main/sample/it_should_draw_code_128_encode_c.png?raw=true)

# EAN-13
```csharp
var result = "9957346284897".Ean13Encode();
var stream = result.DrawPng(result.Count, 100);
```
![ean_13](https://github.com/capeit-pl/barcodes/blob/main/sample/it_should_draw_ean_13.png?raw=true)

# ITF 14
```csharp
var result = "9876543210921".Itf14Encode();
var stream = result.DrawPng(result.Count, 100);
```
![itf_14](https://github.com/capeit-pl/barcodes/blob/main/sample/it_should_draw_itf_14.png?raw=true)

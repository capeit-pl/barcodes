# Code 128
## Type A
```csharp
var result = "Code 128".Code128EncodeTypeA();
var code128Stream = result.DrawPng(code128.Count, 100);
```

## Type B
```csharp
var result = "Code 128".Code128EncodeTypeB();
var code128Stream = result.DrawPng(code128.Count, 100);
```

## Type C
```csharp
var result = "Code 128".Code128EncodeTypeC();
var code128Stream = result.DrawPng(code128.Count, 100);
```

# EAN-13
var result = "9957346284897".Ean13Encode();
var stream = result.DrawPng(result.Count, 100);
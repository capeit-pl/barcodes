# Code 128
## Type A
```csharp
var enoder = new Code128();
var code128 = enoder.EncodeTypeA("Code 128");
var code128Stream = result.DrawPng(code128.Count, 100);
```

## Type B
```csharp
var enoder = new Code128();
var code128 = enoder.EncodeTypeB("Code 128");
var code128Stream = result.DrawPng(code128.Count, 100);
```

## Type A
```csharp
var enoder = new Code128();
var code128 = enoder.EncodeTypeC("Code 128");
var code128Stream = result.DrawPng(code128.Count, 100);
```

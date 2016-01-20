# ConvertModelBinder

ASP.NET5 MVC用のカスタムモデルバインダーです。

## Description

標準のMVCモデルバインディングでは、「1,024」といったカンマ区切りの数値をdecimalにバインドすることができません。  
このConvertModelBinderを用いると、decimalとDateTimeへの柔軟なバインドを行うことができます。

## Features

- decimal, decimal?, DateTime, DateTime?へのバインドの拡張  
※入力値をConvert.ToDecimal、Convert.ToDateTimeで変換しているだけです。  

## Requirement

- ASP.NET5 MVC RC1  

## Usage

Startup.csのConfigureServicesで次のように設定すると、ConvertModelBinderが有効になります。

```
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // Add this
                options.AddConvertModelBinder();
            });
        }
```

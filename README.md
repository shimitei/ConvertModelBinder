# ConvertModelBinder

ASP.NET Core MVC用のカスタムモデルバインダーです。

## Description

標準のMVCモデルバインディングでは、「1,024」といったカンマ区切りの数値をdecimalにバインドすることができません。  
このConvertModelBinderを用いると、decimalへの柔軟なバインドを行うことができます。

## Features

- decimal, decimal?, ~~DateTime, DateTime?~~へのバインドの拡張  
※入力値をConvert.ToDecimalで変換しているだけです。  
※RC2ではConvert.ToDateTimeを通さなくても同様にバインドされたため外しました。  

## Requirement

- ASP.NET Core MVC RC2  

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

# ConvertModelBinder

ASP.NET5 MVC(beta6)用のカスタムモデルバインダーです。

## Description

標準のMVCモデルバインディングでは、「1,024」といったカンマ区切りの数値をdecimalにバインドすることができません。  
このConvertModelBinderを用いると、decimalとDateTimeへの柔軟なバインドを行うことができます。

## Features

- decimal, decimal?, DateTime, DateTime?へのバインドの拡張  
※入力値をConvert.ToDecimal、Convert.ToDateTimeで変換しているだけです。  

## Requirement

- ASP.NET5 MVC beta6  
※beta7以降の動作は未確認です。

## Usage

Startup.csのConfigureServicesで次のように設定すると、ConvertModelBinderが有効になります。

```
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.ConfigureMvc(options =>
            {
                // Add this
                options.AddConvertModelBinder();
            });
        }
```

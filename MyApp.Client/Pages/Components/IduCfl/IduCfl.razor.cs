//using MudBlazor;

//namespace MyApp.Client.Pages.Components;

//public class IduCfl
//{
//}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyApp.Shared;
using System.Linq.Expressions;

namespace MyApp.Client.Pages.Components.IduCfl;

public partial class IduCfl
{
    private async Task OnAdornmentClickIduCflSingleAsync()
    {
        //DataGridCustomFormEditingModel newElement = new();

        var parameters = new DialogParameters<IduCflDataGrid>();
        parameters.Add(x => x.ChooseType, ChooseType);

        DialogOptions options = new DialogOptions()
        {
            //FullWidth = true,
            MaxWidth = MaxWidth.ExtraSmall,
            CloseButton = true,
            DisableBackdropClick = true,
            NoHeader = true,
            Position = DialogPosition.Center,
            CloseOnEscapeKey = true,
            FullScreen = true,
        };

        var dialog = await DialogService.ShowAsync<IduCflDataGrid>("Add", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            if (result.Data is List<WeatherListItemDto> resultArray)
            {
                var texts = (from T0 in resultArray 
                             select T0.Summary
                   ).ToList();

                BindingValue = $"{string.Join(", ", texts.Select(x => x))}";
                await _textField.Validate();
            }
        }
    }

    private MudTextField<String> _textField { get; set; }

    [Parameter]
    public string ChooseType { get; set; }

    [Parameter]
    public string Label { get; set; }


    private string _value;

    [Parameter]
    public string BindingValue
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            BindingValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<string> BindingValueChanged { get; set; }


    [Parameter]
    [Category(CategoryTypes.FormComponent.Validation)]
    public Expression<Func<string>>? For { get; set; }

}


# Fable.Import.Downshift

This package provides Fable bindings for [downshift](https://github.com/paypal/downshift).

## Installation

* Install the `downshift` npm package:
  * using npm: `npm install downshift`
  * using yarn: `yarn add downshift`

* Install the bindings:
  * using dotnet: `dotnet add package Fable.Import.Downshift`
  * using paket: `paket add Fable.Import.Downshift`

## Usage

Familiarity with [downshift’s native JS API ](https://github.com/paypal/downshift) and how downshift works is assumed. Fable.Import.Downshift simply provide a Fable-React idiomatic API with most props as DU cases.

As you might expect from downshift’s native API, the `downshift` helper component does not accept a list of children, but a function that receives the downshift state/helpers and returns a React component.

The downshift prop getters are functions `#seq<IHTMLProp> -> seq<IHTMLProp>` where the input props are passed to the respective prop getter. (The only exception is `getItemProps`, which also takes `'Item` as its first parameter since it’s a required prop.)

Below is the [downshift readme example](https://github.com/paypal/downshift/blob/39ca5d449e25dfe89b8cbd58b1c6f5a8d3e04329/README.md#usage) converted to F#:

```f#
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import
open Fable.Import.Downshift

type Item = { Value: string }

let items = 
  [ { Value = "apple" }
    { Value = "pear" }
    { Value = "orange" }
    { Value = "grape" }
    { Value = "banana" } ]

let view =
  downshift [
    OnChange (fun item _ -> Browser.window.alert(sprintf "You selected %s" item.Value))
    ItemToString (function Some item -> item.Value | None -> "")
  ] (fun ds ->
    div [] [
      label (ds.getLabelProps []) [ str "Enter a fruit" ]
      input (ds.getInputProps [])
      ul (ds.getMenuProps []) [
        if ds.isOpen then
          yield
            items
            |> List.filter (fun item -> item.Value.Contains ds.inputValue)
            |> List.mapi (fun index item ->
                li (ds.getItemProps item [
                  Key item.Value
                  Index index
                  Style [
                    BackgroundColor 
                      (if ds.highlightedIndex = Some index then "lightgray" else "white")
                    FontWeight (if ds.selectedItem = Some item then "bold" else "normal")
                  ]
                ]) [
                  str item.Value
                ]
            )
            |> ofList
      ]
    ]
  )

```

### Props specific to downshift

* The DU type `DownshiftProps` contains props you can pass to the `downshift` helper.
* The DU types `GetInputPropsOptions`, `GetItemPropsOptions`, etc. contains props you can pass to the respective prop getters.
  * Note that `GetItemPropsOptions` does not contain an `Item` case; as mentioned previously, since this is a required prop, it’s passed as the first argument to `getItemProps`.
* The special `SuppressRefError` prop is available in the DU type `GetPropsCommonOptions`. You pass this as a normal prop alongside others, and the binding internals takes care of passing that prop as a separate object as required by the downshift API.

Changelog
---------

#### 1.1.1 (2019-07-25)

- Added femto support

#### 1.1.0 (2019-05-02)

* Updated for Fable.Core 3, Fable.React 5 and downshift 3.2

#### 1.0.0 (2019-01-12)

* Initial release

## Deployment checklist

1. Make necessary changes to the code
2. Update the changelog
3. Update the version and release notes in the package info
4. Update the supported npm dependency versions for femto in the fsproj
5. Commit and tag the commit (this is what triggers deployment from  AppVeyor). For consistency, the tag should ideally be in the format `v1.2.3`.
6. Push the changes and the tag to the repo. If AppVeyor build succeeds, the package is automatically published to NuGet.

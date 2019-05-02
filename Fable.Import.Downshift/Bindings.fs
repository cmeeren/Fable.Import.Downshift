module Fable.Import.Downshift

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props


let resetIdCounter () : unit =
  import "resetIdCounter" "downshift"


type [<StringEnum>] [<RequireQualifiedAccess>] StateChangeTypes =
  | [<CompiledName "__autocomplete_unknown__">] Unknown
  | [<CompiledName "__autocomplete_mouseup__">] MouseUp
  | [<CompiledName "__autocomplete_item_mouseenter__">] ItemMouseEnter
  | [<CompiledName "__autocomplete_keydown_arrow_up__">] KeyDownArrowUp
  | [<CompiledName "__autocomplete_keydown_arrow_down__">] KeyDownArrowDown
  | [<CompiledName "__autocomplete_keydown_escape__">] KeyDownEscape
  | [<CompiledName "__autocomplete_keydown_enter__">] KeyDownEnter
  | [<CompiledName "__autocomplete_click_item__">] ClickItem
  | [<CompiledName "__autocomplete_blur_input__">] BlurInput
  | [<CompiledName "__autocomplete_change_input__">] ChangeInput
  | [<CompiledName "__autocomplete_keydown_space_button__">] KeyDownSpaceButton
  | [<CompiledName "__autocomplete_click_button__">] ClickButton
  | [<CompiledName "__autocomplete_blur_button__">] BlurButton
  | [<CompiledName "__autocomplete_controlled_prop_updated_selected_item__">] ControlledPropUpdatedSelectedItem
  | [<CompiledName "__autocomplete_touchstart__">] TouchStart

type DownshiftState<'Item> =
  abstract highlightedIndex: int option with get, set
  abstract inputValue: string with get, set
  abstract isOpen: bool with get, set
  abstract selectedItem: 'Item option with get, set

type StateChanges<'Item> =
  abstract ``type``: StateChangeTypes with get, set
  inherit DownshiftState<'Item>

type Actions<'Item> =
  abstract clearItems: (unit -> unit) with get, set
  abstract clearSelection: ((unit -> unit) -> unit) with get, set
  abstract closeMenu: ((unit -> unit) -> unit) with get, set
  abstract itemToString: ('Item option -> string) with get, set
  abstract openMenu: ((unit -> unit) -> unit) with get, set
  abstract reset: (obj -> (unit -> unit) -> unit) with get, set
  abstract selectHighlightedItem: (obj -> (unit -> unit) -> unit) with get, set
  abstract selectItem: ('Item -> obj -> (unit -> unit) -> unit) with get, set
  abstract selectItemAtIndex: (int -> obj -> (unit -> unit) -> unit) with get, set
  abstract setHighlightedIndex: (int -> obj -> (unit -> unit) -> unit) with get, set
  abstract setItemCount: (int -> unit) with get, set
  abstract setState: (obj -> unit) with get, set
  abstract toggleMenu: (obj -> (unit -> unit) -> unit) with get, set
  abstract unsetItemCount: (unit -> unit) with get, set

type StateAndHelpers<'Item> =
  inherit Actions<'Item>
  inherit DownshiftState<'Item>
  abstract getInputProps: (#seq<IHTMLProp> -> seq<IHTMLProp>) with get, set
  abstract getItemProps: ('Item -> #seq<IHTMLProp> -> seq<IHTMLProp>) with get, set
  abstract getLabelProps: (#seq<IHTMLProp> -> seq<IHTMLProp>) with get, set
  abstract getMenuProps: (#seq<IHTMLProp> -> seq<IHTMLProp>) with get, set
  abstract getRootProps: (#seq<IHTMLProp> -> seq<IHTMLProp>) with get, set
  abstract getToggleButtonProps: (#seq<IHTMLProp> -> seq<IHTMLProp>) with get, set

type A11yStatusMessageOptions<'Item> =
  abstract highlightedIndex: int option with get, set
  abstract highlightedItem: 'Item option with get, set
  abstract inputValue: string with get, set
  abstract isOpen: bool with get, set
  abstract itemToString: ('Item option -> string) with get, set
  abstract previousResultCount: int with get, set
  abstract resultCount: int with get, set
  abstract selectedItem: 'Item option with get, set

type DownshiftProps<'Item> =
  | Children of (StateAndHelpers<'Item> -> ReactElement)
  | DefaultHighlightedIndex of int
  | DefaultIsOpen of bool
  | Environment of Browser.Types.Window
  | GetA11yStatusMessage of (A11yStatusMessageOptions<'Item> -> string)
  | GetItemId of (int -> string)
  | HighlightedIndex of int option
  | Id of string
  | InitialHighlightedIndex of int option
  | InitialInputValue of string
  | InitialIsOpen of bool
  | InitialSelectedItem of U2<'Item option, 'Item list>
  | InputId of string
  | InputValue of string
  | IsOpen of bool
  | ItemCount of int
  | ItemToString of ('Item option -> string)
  | LabelId of string
  | MenuId of string
  | OnChange of ('Item -> StateAndHelpers<'Item> -> unit)
  | OnInputValueChange of (string -> StateAndHelpers<'Item> -> unit)
  | OnOuterClick of (StateAndHelpers<'Item> -> unit)
  | OnSelect of ('Item -> StateAndHelpers<'Item> -> unit)
  | OnStateChange of (StateChanges<'Item> -> StateAndHelpers<'Item> -> unit)
  | OnUserAction of (StateChanges<'Item> -> StateAndHelpers<'Item> -> unit)
  | ScrollIntoView of (Browser.Types.HTMLElement -> Browser.Types.HTMLElement -> unit)
  | SelectedItem of 'Item option
  | SelectedItemChanged of ('Item -> 'Item -> bool)
  | StateReducer of (DownshiftState<'Item> -> StateChanges<'Item> -> DownshiftState<'Item>)
  | SuppressRefError of bool
  interface IHTMLProp

type GetPropsCommonOptions =
  | SuppressRefError of bool
  interface IHTMLProp

type GetInputPropsOptions =
  | Disabled of bool
  interface IHTMLProp

type GetItemPropsOptions =
  | Index of int
  | IsSelected of bool
  | Disabled of bool
  interface IHTMLProp

type GetMenuPropsOptions =
  | RefKey of string
  | [<CompiledName("aria-label")>] AriaLabel of string
  interface IHTMLProp

type GetRootPropsOptions =
  | RefKey of string
  interface IHTMLProp

type GetToggleButtonPropsOptions =
  | Disabled of bool
  interface IHTMLProp


module Internal =

  type RawPropGetters<'Item> =
    abstract getInputProps: (obj -> obj) with get, set
    abstract getItemProps: (obj -> obj) with get, set
    abstract getLabelProps: (obj -> obj) with get, set
    abstract getMenuProps: (obj -> obj -> obj) with get, set
    abstract getRootProps: (obj -> obj -> obj) with get, set
    abstract getToggleButtonProps: (obj -> obj) with get, set

  type RawStateAndHelpers<'Item> =
    inherit RawPropGetters<'Item>
    inherit Actions<'Item>
    inherit DownshiftState<'Item>

  type RawDownshiftProps<'Item> =
    | Children of (RawStateAndHelpers<'Item> -> ReactElement)
    | DefaultHighlightedIndex of int option
    | DefaultIsOpen of bool
    | Environment of Browser.Types.Window
    | GetA11yStatusMessage of (A11yStatusMessageOptions<'Item> -> string) option
    | GetItemId of (int -> string)
    | HighlightedIndex of int option
    | Id of string
    | InitialHighlightedIndex of int option
    | InitialInputValue of string
    | InitialIsOpen of bool
    | InitialSelectedItem of U2<'Item option, 'Item list>
    | InputId of string
    | InputValue of string
    | IsOpen of bool
    | ItemCount of int
    | ItemToString of ('Item option -> string)
    | LabelId of string
    | MenuId of string
    | OnChange of ('Item -> RawStateAndHelpers<'Item> -> unit)
    | OnInputValueChange of (string -> RawStateAndHelpers<'Item> -> unit)
    | OnOuterClick of (RawStateAndHelpers<'Item> -> unit)
    | OnSelect of ('Item -> RawStateAndHelpers<'Item> -> unit)
    | OnStateChange of (StateChanges<'Item> -> RawStateAndHelpers<'Item> -> unit)
    | OnUserAction of (StateChanges<'Item> -> RawStateAndHelpers<'Item> -> unit)
    | ScrollIntoView of (Browser.Types.HTMLElement -> Browser.Types.HTMLElement -> unit)
    | SelectedItem of 'Item option
    | SelectedItemChanged of ('Item -> 'Item -> bool) option
    | StateReducer of (DownshiftState<'Item> -> StateChanges<'Item> -> DownshiftState<'Item>) option
    | SuppressRefError of bool option
    interface IHTMLProp

  [<Emit("
  var keys = [];
  for(var key in $0){ keys.push([key, $0[key]]); }
  return keys;
  ")>]
  let toKeyValueList (o: obj) : (string * obj) list = jsNative

  let stateAndHelpersOfRaw (raw: RawStateAndHelpers<'Item>) : StateAndHelpers<'Item> =

    // Helper to convert the props to an obj, pass to rawFun, and convert back to seq<IHTMLProp>.
    let rawProps (rawFun: obj -> obj) : seq<IHTMLProp> -> seq<IHTMLProp> =
      keyValueList CaseRules.LowerFirst
      >> rawFun
      >> toKeyValueList
      >> Seq.map (fun kv -> kv |> HTMLAttr.Custom :> IHTMLProp)

    // Same as above, but used for functions accepting a second object
    // with suppressRefError = true.
    let rawPropsExtra (rawFun: obj -> obj -> obj) (props: seq<IHTMLProp>) : seq<IHTMLProp> =
      let hasSuppressRefError =
        props
        |> Seq.choose (function :? GetPropsCommonOptions as x -> Some x | _ -> None)
        |> Seq.exists (function GetPropsCommonOptions.SuppressRefError true -> true | _ -> false)
      let filteredProps =
        props
        |> Seq.filter (function :? GetPropsCommonOptions -> false | _ -> true)
        |> keyValueList CaseRules.LowerFirst
      let extraProps = createEmpty<obj>
      if hasSuppressRefError then extraProps?suppressRefError <- true
      rawFun filteredProps extraProps
      |> toKeyValueList
      |> Seq.map (fun kv -> kv |> HTMLAttr.Custom :> IHTMLProp)

    let s = JS.Object.assign(createEmpty<_>, raw) :?> StateAndHelpers<'Item>
    s.getInputProps <- (fun props -> props |> rawProps raw.getInputProps)
    s.getItemProps <- (fun item props -> props |> Seq.append [HTMLAttr.Custom ("item", item)] |> rawProps raw.getItemProps)
    s.getLabelProps <- (fun props -> props |> rawProps raw.getLabelProps)
    s.getMenuProps <- (fun props -> props |> rawPropsExtra raw.getMenuProps)
    s.getRootProps <- (fun props -> props |> rawPropsExtra raw.getRootProps)
    s.getToggleButtonProps <- (fun props -> props |> rawProps raw.getToggleButtonProps)
    s

  let downshiftPropToRaw (prop: DownshiftProps<'Item>) : RawDownshiftProps<'Item> =
    match prop with
    | DownshiftProps.Children f -> RawDownshiftProps.Children (stateAndHelpersOfRaw >> f)
    | DownshiftProps.OnChange f -> RawDownshiftProps.OnChange (fun i s -> f i (stateAndHelpersOfRaw s))
    | DownshiftProps.OnInputValueChange f -> RawDownshiftProps.OnInputValueChange (fun str s -> f str (stateAndHelpersOfRaw s))
    | DownshiftProps.OnOuterClick f -> RawDownshiftProps.OnOuterClick (stateAndHelpersOfRaw >> f)
    | DownshiftProps.OnSelect f -> RawDownshiftProps.OnSelect (fun i s -> f i (stateAndHelpersOfRaw s))
    | DownshiftProps.OnStateChange f -> RawDownshiftProps.OnStateChange (fun o s -> f o (stateAndHelpersOfRaw s))
    | DownshiftProps.OnUserAction f -> RawDownshiftProps.OnUserAction (fun o s -> f o (stateAndHelpersOfRaw s))
    | p -> !!p


let inline downshift<'Item> (b : seq<DownshiftProps<'Item>>) (c: StateAndHelpers<'Item> -> ReactElement) : ReactElement =
  let b = Seq.append b [DownshiftProps.Children c]
  let b = b |> Seq.map (fun d -> d |> Internal.downshiftPropToRaw)
  ofImport "default" "downshift" (keyValueList CaseRules.LowerFirst b) []

module Main

open Fable.Core.JsInterop
open Pages

importSideEffects "./styles.css"

// register your custom elements here
FeedManager.register ()
FeedViewer.register ()
Home.register ()
App.register ()

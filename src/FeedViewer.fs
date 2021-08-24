[<RequireQualifiedAccess>]
module Pages.FeedViewer

open Browser.Types

open Lit
open Fable.Haunted

open Types

let private goBack (event: Event) =
    let evt =
        Haunted.createEvent (
            "x-go-back",
            {| bubbles = true
               composed = true
               cancelable = true |}
        )

    dispatchEvent evt event.target

let private view (props: {| feed: Feed option |}) =

    let feed =
        defaultArg props.feed { Name = ""; Url = "" }

    html
        $"""
        <ion-toolbar>
            <ion-title>{feed.Name}</ion-title>
            <ion-buttons slot="secondary">
                <ion-button fill="solid" @click={goBack}>
                    <ion-icon name="arrow-back-outline"></ion-icon>
                    Back
                </ion-button>
            </ion-buttons>
        </ion-toolbar>
        <section>

        </section>
        """

let register () =
    defineComponent "x-feed-viewer" (Haunted.Component view)

[<RequireQualifiedAccess>]
module App

open Lit

open Fable.Haunted

let private app () =

    html
        $"""
        <ion-app>
            <ion-tabs>
                <ion-tab tab="home" component="x-home"></ion-tab>
                <ion-tab tab="feeds" component="x-feed-manager"></ion-tab>
                <ion-tab-bar slot="bottom">
                    <ion-tab-button tab="home">
                        <ion-icon name="home"></ion-icon>
                        <ion-label>Home</ion-label>
                    </ion-tab-button>
                    <ion-tab-button tab="feeds">
                        <ion-icon name="bookmark"></ion-icon>
                        <ion-label>Feeds</ion-label>
                    </ion-tab-button>
                </ion-tab-bar>
            </ion-tabs>
        </ion-app>
        """

let register () =
    defineComponent "flit-app" (Haunted.Component app)

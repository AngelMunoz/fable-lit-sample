# Lit HTML + Fable!

[Haunted]: https://github.com/AngelMunoz/Fable.Haunted
[Lit]: https://lit.dev/docs/libraries/standalone-templates/
[Elmish.Lit]: https://github.com/alfonsogarciacaro/Elmish.Lit
[Ionic Framework]: https://ionicframework.com/

A simple example of high interoperation with other UI libraries based on web components, in this case we used the popular [Ionic Framework] to create a simple rss... not reader but at least shows the list of items of an rss feed.

Too stringy for your eyes? check out [vscode-template-fsharp-highlight](https://marketplace.visualstudio.com/items?itemName=alfonsogarciacaro.vscode-template-fsharp-highlight)

**[Haunted] + [Elmish.Lit] + [Ionic Framework]|> 💖**

### Run

```bash
pnpm install # or npm install
pnpm start # or npm start
```

things to look out for

- Function based programming

  As we're used in F# we can use functions as the base block for our SPA's, you are not required to use classes or similar concepts as you would have to do with many frontend frameworks

- Web Components

  In this sample every "page" is a web component it doesn't need to, but it can be as well as any other function can be converted into a web component with very very little effort, this can help your organization to share components between teams if you're building a brand the best part of all is that your team won't need to use F# to consume those components, so no need to convert to 100% from day zero

- Event Handling

  No need for callback passing, events and custom events bubble naturally in the browser and we can leverage that instead of drilling callback props

- No Bindings Required for Ionic (or very likely any other web component library)

  This might be the most important point, there isn't a single binding here for ionic framework because we're using HTML, so in contrast of DSL based html you don't need to write extra bindings for your custom elements

- Data Binding

  You can easily bind properties or attributes to provide props to your render functions
  this makes it easier to create self contained components rather than mega components that do too much

- Html Templates

  In case that for some reason you need to migrate away from F# your HTML templates can be almost copy pasted on any [Lit] application with javascript or typescript so you are not locked in, the same can be done from typescript/javascript to F# the biggest change is on string interpolation `${}` vs `{}` otherwise attributes, events or data binding can be used as is used in Lit templates

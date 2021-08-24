// Snowpack Configuration File
// See all supported options: https://www.snowpack.dev/reference/configuration
/** @type {import("snowpack").SnowpackUserConfig } */
module.exports = {
  mount: {
    public: { url: '/', static: true },
    src: { url: '/dist', static: true },
    'node_modules/@shoelace-style/shoelace/dist/assets': { url: '/assets', static: true },
  },
  plugins: [],
  routes: [],
  optimize: {
    /* Example: Bundle your final build: */
    bundle: true,
    splitting: true,
    treeshake: true,
    manifest: true,
    target: 'es2017',
    minify: true
  },
  packageOptions: {
    /* ... */
    knownEntrypoints: ['@stencil/core/internal/client']
  },
  devOptions: {
    /* ... */
  },
  buildOptions: {
    /* ... */
    clean: true,
    out: "dist"
  },
  exclude: [
    "**/*.{fs,fsproj}",
    "**/bin/**",
    "**/obj/**"
  ],
  /* ... */
};
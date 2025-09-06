// https://nuxt.com/docs/api/configuration/nuxt-config
// file: nuxt.config.ts

export default defineNuxtConfig({
   compatibilityDate: '2025-09-06',
  // Modules for Tailwind CSS and Google Fonts
  modules: [
    '@nuxtjs/tailwindcss',
    '@nuxtjs/google-fonts'
  ],

  // Google Fonts configuration
  googleFonts: {
    families: {
      Inter: ['400', '700'] // Load Inter font with weights 400 and 700
    }
  },

  // App-wide configuration
  app: {
    head: {
      title: 'Trim | Find Your Perfect Artist',
      link: [
        // Add Leaflet's CSS globally
        {
          rel: 'stylesheet',
          href: 'https://unpkg.com/leaflet@1.9.4/dist/leaflet.css',
          integrity: 'sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY=',
          crossorigin: ''
        }
      ]
    }
  },

  // Developer tools
  devtools: { enabled: true }
})
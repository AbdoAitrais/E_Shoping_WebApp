/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.cshtml',
    './Views/**/*.cshtml'
  ],
  theme: {
    extend: {
      colors: {
        "amazon": "#0F1111",
        "amazon2": "#232f3e"
      }
    },
  },
  plugins: [],
}


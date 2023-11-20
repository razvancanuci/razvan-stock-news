/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    screens: {
      sm: '480px',
      md: '768px',
      lg: '976px',
      xl: '1440px'
    },
    extend: {
      margin: {
        '1/5': '20%',
        '1/10': '10%',
        '1/4': '25%',
        '1/2': '50%',
        '1/3': '34%',
        '1/20': '5%',
        '1/40': '2.5%'
      },
    },
  },
  plugins: [],
}

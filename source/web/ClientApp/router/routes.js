import HomePage from 'components/home-page'
import BuiltWith from 'components/built-with'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'builtwith', path: '/builtwith', component: BuiltWith, display: 'Built With Info', icon: 'graduation-cap' },
]

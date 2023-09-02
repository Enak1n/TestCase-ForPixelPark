import { useState } from 'react'
import './app/assets/styles/index.css'
import AppRouter from './app/components/AppRouter'
import Logo from './app/components/Logo'
import { AppContext } from './context/context'

function App() {
	const [isSuccess, setIsSuccess] = useState<boolean>(false)

	return (
		<AppContext.Provider value={{ isSuccess, setIsSuccess }}>
			<div>
				<Logo />
				<AppRouter />
			</div>
		</AppContext.Provider>
	)
}

export default App

import { FC, useContext } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom'
import { AppContext } from '../../context/context'
import MainPage from '../../pages/MainPage/MainPage'
import SuccessPage from '../../pages/SuccessPage/SuccessPage'

const AppRouter: FC = () => {
	const { isSuccess } = useContext(AppContext)

	return (
		<Routes>
			{isSuccess ? (
				<Route Component={SuccessPage} path='/success' />
			) : (
				<Route Component={MainPage} path='/' />
			)}
			<Route path='*' element={<Navigate to='/' replace />} />
		</Routes>
	)
}

export default AppRouter

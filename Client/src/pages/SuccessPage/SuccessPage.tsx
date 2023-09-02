import { Button, Result } from 'antd'
import { FC, useContext } from 'react'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../../context/context'

const SuccessPage: FC = () => {

	const navigate = useNavigate()
	const { setIsSuccess } = useContext(AppContext)

	function goBack (){
		setIsSuccess(false)
		navigate('/')
	}

	return (
		<div className='success-page'>
			<Result
				status='success'
				title='Вы успешно подтвердили электронную почту'
				extra={[<Button onClick={goBack}>Вернуться назад</Button>]}
			/>
		</div>
	)
}

export default SuccessPage

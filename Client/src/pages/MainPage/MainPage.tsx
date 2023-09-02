import { message } from 'antd'
import axios from 'axios'
import { useContext, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import MyInput from '../../app/components/UI/MyInput'
import { AppContext } from '../../context/context'

type InputStatysType = '' | 'error' | 'warning' | undefined

const MainPage = () => {
	const { setIsSuccess } = useContext(AppContext)
	const navigate = useNavigate()
	const [messageApi, contextHolder] = message.useMessage()
	const [emailValue, setEmailValue] = useState('')
	const [isSendCode, setIsSendCode] = useState(false)
	const [isLoadingEmail, setIsLoadingEmail] = useState(false)
	const [isLoadingCode, setIsLoadingCode] = useState(false)
	const [codeValue, setCodeValue] = useState('')
	const [emailInputStatus, setEmailInputStatus] = useState<InputStatysType>('')
	const [codeInputStatus, setCodeInputStatus] = useState<InputStatysType>('')

	async function submitCode() {
		if (emailValue.length > 0) {
			setEmailInputStatus('')
			try {
				setIsLoadingEmail(true)
				const response = await axios.get('http://localhost:8000/sendcode', {
					params: { email: emailValue },
				})
				if (response.status === 200) {
					setIsSendCode(true)
					messageApi.open({
						type: 'success',
						content: `На почту ${emailValue} было отправлено письмо с кодом`,
						duration: 3,
					})
				}
			} catch (error) {
				messageApi.open({
					type: 'error',
					content: `${error}`,
					duration: 5,
				})
			} finally {
				setIsLoadingEmail(false)
			}
		} else {
			messageApi.open({
				type: 'error',
				content: 'Пожалуйста заполните поле для ввода электронной почты',
				duration: 3,
			})

			setEmailInputStatus('error')
		}
	}

	async function checkCode() {
		if (codeValue.length > 0) {
			try {
				setIsLoadingCode(true)
				setCodeInputStatus('')
				const response = await axios.get('http://localhost:8000/checkCode', {
					params: { code: codeValue, email: emailValue },
				})
				if (response.status === 200) {
					setIsSuccess(true)
					navigate('/success')
				}
				// Обнуляем значения email и код
				setCodeValue('')
				setEmailValue('')
			} catch (error) {
				messageApi.open({
					type: 'error',
					content: `${error}`,
					duration: 5,
				})
				setCodeInputStatus('error')
			} finally {
				setIsLoadingCode(false)
			}
		} else {
			messageApi.open({
				type: 'error',
				content: 'Пожалуйста заполните поле для ввода кода',
				duration: 3,
			})
			setCodeInputStatus('error')
		}
	}

	return (
		<div className='main-page-block'>
			{contextHolder}
			<MyInput
				type='email'
				loading={isLoadingEmail}
				className='mail-input'
				enterButton='Отправить код'
				inputStatus={emailInputStatus}
				onChangeFunction={e => {
					setEmailValue(e.target.value)
				}}
				onSearchFunction={submitCode}
				placeholder='Введите электоронную почту'
				value={emailValue}
			/>
			{isSendCode && (
				<MyInput
					type='text'
					loading={isLoadingCode}
					className='code-input'
					enterButton='Проверить код'
					inputStatus={codeInputStatus}
					onChangeFunction={e => {
						setCodeValue(e.target.value)
					}}
					onSearchFunction={checkCode}
					placeholder='Введите код с почты'
					value={codeValue}
				/>
			)}
		</div>
	)
}

export default MainPage

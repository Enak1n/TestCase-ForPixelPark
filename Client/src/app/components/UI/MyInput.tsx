import { Input } from 'antd'
import { FC } from 'react'

const { Search } = Input

interface MyInputProps {
	loading: boolean
	type: 'email' | 'text'
	inputStatus: '' | 'error' | 'warning' | undefined
	onSearchFunction: () => void
	onChangeFunction: (e: any) => void
	className: string
	placeholder: string
	enterButton: string
	value: string
}

const MyInput: FC<MyInputProps> = ({
	type,
	loading,
	className,
	enterButton,
	inputStatus,
	onChangeFunction,
	onSearchFunction,
	placeholder,
	value,
}) => {
	return (
		<Search
			type={type}
			loading={loading}
			status={inputStatus}
			onSearch={onSearchFunction}
			value={value}
			onChange={e => onChangeFunction(e)}
			className={className}
			placeholder={placeholder}
			enterButton={enterButton}
			size='large'
		/>
	)
}

export default MyInput

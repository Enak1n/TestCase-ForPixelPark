import { FC } from 'react'

const Logo: FC = () => {
	return (
		<div className='logo'>
			<a target='_blank' href='https://pixlpark.ru/'>
				<img className='logo__img' src='/pixlpark_logo.svg' />
			</a>
		</div>
	)
}

export default Logo

import { useEffect } from "react";
import { useForm } from "react-hook-form"
import { useMutation, useQueryClient } from "react-query";
import { API } from "../Api";
import { useAuth } from "../auth";
import './login-view.css'

export function LoginView() {

	const queryClient = useQueryClient()
	const { signIn } = useAuth();
	const { handleSubmit, register, formState } = useForm({ defaultValues: { username: '', password: '' } });
	

	const mutation = useMutation<any, any, any>(loginform => {
		return API.login(loginform)
	}, {
		onSuccess: (result) => {
			if(result.success) {
				signIn(result.data);
				queryClient.invalidateQueries('me');
			} else {
				// TODO: show error message
			}
		}
	})


	return (
		<div className="login-screen">
			{
				mutation.error ? (<div className="error-message">Login fail try again</div>) : null
			}

			<form onSubmit={handleSubmit((form) => mutation.mutate(form))}>
				<div>
					<label htmlFor="username">Username : </label>
					<input placeholder="Username" type="text" {...register("username", { required: true })} />
				</div>

				<div>
					<label htmlFor="password">Password : </label>
					<input placeholder="Password" type="password" {...register("password", { required: true })} />
				</div>
				<button type="submit">Login</button>
			</form>

			<div className="testing">
				Testing user:
				<br />
				QA:   elysia - 123
				<br />
				RD:   kevin - 321
			</div>

		</div>
	)
}
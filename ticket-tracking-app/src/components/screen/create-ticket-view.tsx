import { useForm } from "react-hook-form";
import { useMutation } from "react-query";
import { useNavigate, useNavigation } from "react-router-dom";
import { API } from "../Api";


export function CreateTicketView() {
	const navi = useNavigate()
	const { handleSubmit, register, formState } = useForm({ defaultValues: { summery: '', description: '', type: 0 } });

	const mutation = useMutation<any, any, any>(ticket => {
		return API.createTicket({ ...ticket, type: parseInt(ticket.type) })
	}, {
		onSuccess: () => {
			navi('/');
		}
	})

	return (
		<div>
			<form className="form-ticket-creation" onSubmit={handleSubmit((form) => mutation.mutate(form))} >
				<div>
					<label htmlFor="Summery">Summery : </label>
					<input placeholder="summery" type="text" {...register("summery", { required: true })} />
				</div>

				<div className="form-align-top">
					<label htmlFor="description">Description : </label>
					<textarea placeholder="description"  {...register("description", { required: true })} ></textarea>
				</div>

				<div>
					<label>Ticket Type : </label>
					<select {...register("type", { required: true })} >
						<option value="0">Bug</option>
					</select>
				</div>

				<button type="submit">Create Ticket</button>
			</form>
		</div>


	)
}


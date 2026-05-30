<script lang="ts">
	import type { TimetableInfos } from '$lib/types/db_raw_types';
	import { get_timetables } from '$lib/utils/db_operations';
	import { onMount } from 'svelte';

	interface AvailableTimetableGridProps {
		access_token: string;
	}

	let { access_token }: AvailableTimetableGridProps = $props();
	let availableTimetables: TimetableInfos = $state([]);
	onMount(async () => {
		const timetable_request = await get_timetables(access_token);

		if (timetable_request.isOk()) {
			availableTimetables = timetable_request.value;
		}
	});
</script>

<div class="grid grid-cols-3 gap-4">
	{#each availableTimetables as timetable (timetable.id)}
		<div class="card w-full bg-base-300 card-border">
			<div class="card-body">
				<h2 class="card-title">{timetable.name}</h2>
				<p>
					Timetable for AY{timetable.academicYear}, Semester {timetable.semester}
				</p>
				<div class="card-actions justify-end">
					<button class="btn btn-primary">Edit Timetable</button>
				</div>
			</div>
		</div>
	{/each}
</div>

<script lang="ts">
	import { currentlySelectedMods } from '$lib/shared/shared.svelte';
	import type { TimetableWithMetadata } from '$lib/types/db_raw_types';
	import { removeModEntry } from '$lib/utils/format_db_information';
	import { X } from '@lucide/svelte';
	interface ModListModInfoProps {
		timetable: TimetableWithMetadata;
	}
	let { timetable }: ModListModInfoProps = $props();
	let lesson_groups = $derived(Object.groupBy(timetable.metaData, (x) => x.moduleCode));
	let lesson_headers = $derived(Object.keys(lesson_groups));
</script>

{#each lesson_headers as lesson (lesson)}
	<div class="card w-full bg-base-300 card-border">
		<div class="flex justify-between p-4">
			<div class="flex gap-2">
				<button class="flex-initial {lesson_groups[lesson]![0].colour} badge"></button>
				<div class="flex-none">{lesson}</div>
			</div>

			<X
				onclick={() => {
					currentlySelectedMods.set(
						removeModEntry(
							$currentlySelectedMods,
							timetable.academicYear,
							timetable.semester,
							timetable.id,
							timetable.name,
							lesson_groups[lesson]![0].moduleCode
						)
					);
				}}
			></X>
		</div>
	</div>
{/each}

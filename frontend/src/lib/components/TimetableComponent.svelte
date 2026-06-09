<script lang="ts">
	import { chooseModState } from '$lib/shared/shared.svelte';
	import type { TimetableWithMetadata } from '$lib/types/db_raw_types';
	import { filterTimetableByDay, queryAvailableLessons } from '$lib/utils/format_db_information';
	import { onDestroy, onMount } from 'svelte';
	import TimetableWeekComponent from './TimetableWeekComponent.svelte';
	import type { TimeTableDayInfo } from '$lib/types/internal';
	import type { Unsubscriber } from 'svelte/store';

	const heightOfOneHourLessonPx = 16;

	interface TimetablesProps {
		timetables: TimetableWithMetadata[];
		acadYear: string;
		semester: number;

		timetable_id: string;
		timetable_name: string;
	}
	const { timetables, acadYear, semester, timetable_id, timetable_name }: TimetablesProps =
		$props();

	onDestroy(() => {
		$chooseModState = {
			classNo: '',
			colour: '',
			lessonType: '',
			moduleCode: ''
		};

		chooseModStateCleanUpFunction();
	});

	let chooseModStateCleanUpFunction: Unsubscriber;

	let lmao: { [day: number]: TimeTableDayInfo[] } = $state({});

	onMount(() => {
		chooseModStateCleanUpFunction = chooseModState.subscribe(async (chooseModState) => {
			lmao = {};
			for (let day = 0; day < 5; day++) {
				let mods = await queryAvailableLessons(day, semester, acadYear, chooseModState);

				if (mods.length != 0) {
					lmao[day] = mods;
				}
			}
		});
	});
</script>

{#if Object.keys(lmao).length != 0}
	<div class="grid grid-cols-{Object.keys(lmao).length} grid-rows-12">
		{#each { length: 12 }, y}
			{#each { length: Object.keys(lmao).length }, x}
				<div
					class="col-start-{x + 1} row-start-{y + 1} h-{heightOfOneHourLessonPx} {y % 2 == 0
						? 'bg-base-300'
						: 'bg-base-200'}"
				></div>
			{/each}
		{/each}
		{#each Object.entries(lmao) as [day, tt_info], idx (day)}
			{#await Promise.all( [filterTimetableByDay(Number.parseInt(day), timetables), $state.snapshot(tt_info)] ) then timetableDayInfo}
				<TimetableWeekComponent
					{timetable_id}
					{timetable_name}
					timetableDayDisplayInfo={timetableDayInfo.flat()}
					day={idx}
					{acadYear}
					{semester}
				></TimetableWeekComponent>
			{/await}
		{/each}
	</div>
{:else}
	<div class="grid grid-cols-5 grid-rows-12">
		{#each { length: 12 }, y}
			{#each { length: 5 }, x}
				<div
					class="col-start-{x + 1} row-start-{y + 1} h-{heightOfOneHourLessonPx} {y % 2 == 0
						? 'bg-base-300'
						: 'bg-base-200'}"
				></div>
			{/each}
		{/each}
		{#each { length: 5 }, day}
			{#await Promise.all( [filterTimetableByDay(day, timetables), queryAvailableLessons(day, semester, acadYear, $chooseModState)] ) then timetableDayInfo}
				<TimetableWeekComponent
					{timetable_id}
					{timetable_name}
					timetableDayDisplayInfo={timetableDayInfo.flat()}
					{day}
					{acadYear}
					{semester}
				></TimetableWeekComponent>
			{/await}
		{/each}
	</div>
{/if}

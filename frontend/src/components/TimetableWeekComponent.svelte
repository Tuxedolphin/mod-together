<script lang="ts">
	import TimetableDayComponent from './TimetableDayComponent.svelte';

	import type { Module, RawLesson } from '../types/modules';
	import { currentlySelectedMods } from '../shared/shared.svelte';
	import type { TimeTableDayInfo } from '../types/internal';
	import { normaliseDuration } from '../utils/calculations_for_ui';

	interface WeekTimeTabledComponent {
		day: number;
		modInfo: { [moduleCode: string]: Module };
	}
	const { day, modInfo }: WeekTimeTabledComponent = $props();
	const daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
	const filteredInformation: TimeTableDayInfo[] = $derived(
		calculateOverlappingTimes(filterByDay(modInfo))
	);

	function calculateOverlappingTimes(timeTableInfo: TimeTableDayInfo[]): TimeTableDayInfo[] {
		return timeTableInfo;
	}

	function filterByDay(modInfo: { [moduleCode: string]: Module }): TimeTableDayInfo[] {
		let totalInfo: TimeTableDayInfo[] = [];
		for (const mod in modInfo) {
			const info = modInfo[mod];

			const weekData = info.semesterData.find((semNo) => semNo.semester == 2);
			let ttData = weekData?.timetable.filter((x) => x.day == daysOfWeek[day]);

			const selectedMod = $currentlySelectedMods.selectedMods[mod];

			for (const scheduleInfo in selectedMod) {
				const lessonType = scheduleInfo;
				const classNo = selectedMod[lessonType];
				const lessonForDay = ttData?.filter(
					(x) => x.lessonType == lessonType && x.classNo == classNo
				);

				if (lessonForDay?.length != 0) {
					const lesson = lessonForDay![0] as RawLesson;
					totalInfo.push({
						lessonSchedule: lesson,
						moduleCode: info.moduleCode,
						moduleName: info.title,
						normalisedStartDuration: normaliseDuration('0800', '2000', lesson.startTime),
						normalisedEndDuration: normaliseDuration('0800', '2000', lesson.endTime)
					});
				}
			}
		}

		return totalInfo;
	}
</script>

<div class="relative col-start-{day + 1} row-start-1">
	{#each filteredInformation as mod (mod)}
		<TimetableDayComponent
			lessonSchedule={mod.lessonSchedule}
			moduleCode={mod.moduleCode}
			moduleName={mod.moduleName}
			normalisedStartDuration={mod.normalisedStartDuration}
			normalisedEndDuration={mod.normalisedEndDuration}
		></TimetableDayComponent>
	{/each}
</div>

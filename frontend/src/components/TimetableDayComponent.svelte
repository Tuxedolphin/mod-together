<script lang="ts">
	import type { TimeTableDayInfo } from '../types/internal';
	import { calculateHeightOfClass } from '../utils/calculations_for_ui';

	const {
		lessonSchedule,
		moduleName,
		moduleCode,
		normalisedStartDuration,
		normalisedEndDuration
	}: TimeTableDayInfo = $props();
</script>

<div
	class="absolute mt-{normalisedStartDuration * 192} w-full h-{normalisedEndDuration * 192 -
		normalisedStartDuration * 192} border bg-amber-100 text-xs wrap-break-word"
>
	{moduleCode} - {lessonSchedule.startTime} to {lessonSchedule.endTime}
</div>

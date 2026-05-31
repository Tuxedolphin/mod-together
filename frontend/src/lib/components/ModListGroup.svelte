<script lang="ts">
	import { currentlySelectedMods } from '$lib/shared/shared.svelte';
	import type { TimetableWithMetadata } from '$lib/types/db_raw_types';
	import { onDestroy, onMount } from 'svelte';
	import type { Unsubscriber } from 'svelte/store';
	import ModListModInfo from './ModListModInfo.svelte';
	let current_selected_mods_unsubsriber: Unsubscriber;

	let updated_mod_list: TimetableWithMetadata[] = $state([]);

	onMount(() => {
		current_selected_mods_unsubsriber = currentlySelectedMods.subscribe((new_mods) => {
			updated_mod_list = [...new_mods];
		});
	});

	onDestroy(() => {
		current_selected_mods_unsubsriber();
	});
</script>

<h2 class="p-1 text-2xl">Selected Mods:</h2>

<div class="grid gap-4 p-1 lg:grid-cols-3">
	{#each updated_mod_list as tt (tt.metaData)}
		<ModListModInfo timetable={tt}></ModListModInfo>
	{/each}
</div>

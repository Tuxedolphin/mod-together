<script lang="ts">
	import ModuleCodeSuggestionMini from './ModuleCodeSuggestionMini.svelte';

	import { AllSubstringsIndexStrategy, Search } from 'js-search';
	import type { ModSummary } from '../types/mod_summaries';

	let searchTerm = $state('');
	const { summaries } = $props();
	const modSearch = new Search('moduleCode');
	modSearch.indexStrategy = new AllSubstringsIndexStrategy();
	modSearch.addIndex('moduleCode');
	modSearch.addIndex('title');

	// svelte-ignore state_referenced_locally
	modSearch.addDocuments(summaries);

	let results = $derived(modSearch.search(searchTerm)) as ModSummary[];
</script>

<p>Search</p>
<input type="text" placeholder="Type here" class="input w-full" bind:value={searchTerm} />

<div class="max-h-36 overflow-auto scroll-auto">
	{#each results as res (res.moduleCode)}
		<ModuleCodeSuggestionMini mod={res}></ModuleCodeSuggestionMini>
	{/each}
</div>

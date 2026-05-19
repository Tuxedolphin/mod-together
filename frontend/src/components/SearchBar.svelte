<script lang="ts">
	import { AllSubstringsIndexStrategy, Search } from 'js-search';
	import type { ModSummary } from '../types/mod_summaries';

    let searchTerm = $state("")
    const { summaries } = $props();
	const modSearch = new Search('moduleCode');
	modSearch.indexStrategy = new AllSubstringsIndexStrategy();
	modSearch.addIndex('moduleCode');
	modSearch.addIndex('title');
	
    // svelte-ignore state_referenced_locally
    modSearch.addDocuments(summaries)

    let results = $derived(
        modSearch.search(searchTerm)
    ) as ModSummary[]
</script>

<p>Search</p>
<input type="text" placeholder="Type here" class="input" bind:value={searchTerm} />

{#each results as res (res.moduleCode) }
    <p>{res.moduleCode} {res.title}</p>
{/each}
<script lang="ts">
	import { onMount } from 'svelte';
	import GreetingComponent from '$lib/components/GreetingComponent.svelte';
	import { access_token } from '$lib/shared/shared.svelte';
	import { goto } from '$app/navigation';
	import { resolve } from '$app/paths';
	import AvailableTimetableGrid from '$lib/components/AvailableTimetableGrid.svelte';

	let token = $state('');
	onMount(() => {
		if (!$access_token.access_token) {
			goto(resolve('/login'));
		}

		token = $access_token.access_token;
	});
</script>

{#if token}
	<GreetingComponent access_token={token}></GreetingComponent>
	<AvailableTimetableGrid access_token={token}></AvailableTimetableGrid>
{/if}

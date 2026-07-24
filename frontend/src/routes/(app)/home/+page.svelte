<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import { goto } from "$app/navigation";
  import { resolve } from "$app/paths";
  import AvailableTimetableGrid from "$lib/components/AvailableTimetableGrid.svelte";
  import GreetingComponent from "$lib/components/GreetingComponent.svelte";
  import ImportFromNUSMods from "$lib/components/ImportFromNUSMods.svelte";
  import {
    timetable_list_should_be_refreshed,
    token_information,
  } from "$lib/shared/shared.svelte";
  import CreateNewTimetableButton from "../../../lib/components/CreateNewTimetableButton.svelte";
  import type { TimetableInfos } from "$lib/types/db_raw_types";
  import type { Unsubscriber } from "svelte/store";
  import { get_timetables } from "$lib/utils/db_operations";
  import TimetableList from "../shared/TimetableList.svelte";

  let availableTimetables: TimetableInfos = $state([]);

  let unsubscribe_from_refresh: Unsubscriber;
  onMount(async () => {
    unsubscribe_from_refresh = timetable_list_should_be_refreshed.subscribe(
      async (should_be_refreshed) => {
        if (!should_be_refreshed) return;

        const timetable_request = await get_timetables($token_information.a);
        if (timetable_request.isOk()) {
          availableTimetables = [...timetable_request.value];
        }

        timetable_list_should_be_refreshed.set(false);
      },
    );

    timetable_list_should_be_refreshed.set(true);
  });

  onDestroy(() => {
    if (unsubscribe_from_refresh) {
      unsubscribe_from_refresh();
    }
  });
</script>

{#if $token_information.a}
  <div class="flex items-center justify-between mt-2">
    <GreetingComponent></GreetingComponent>
    <div class="flex gap-2">
      <CreateNewTimetableButton></CreateNewTimetableButton>
      <ImportFromNUSMods></ImportFromNUSMods>
    </div>
  </div>

  <hr class="my-2 h-px border-0 bg-gray-200" />

  <TimetableList {availableTimetables}></TimetableList>
{/if}

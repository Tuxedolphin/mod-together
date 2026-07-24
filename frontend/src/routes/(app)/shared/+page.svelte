<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import type { Unsubscriber } from "svelte/store";
  import {
    timetable_list_should_be_refreshed,
    token_information,
  } from "$lib/shared/shared.svelte";
  import type { TimetableInfos } from "$lib/types/db_raw_types";
  import { get_shared_timetables } from "$lib/utils/db_operations";
  import TimetableList from "./TimetableList.svelte";

  let availableTimetables: TimetableInfos = $state([]);

  let unsubscribe_from_refresh: Unsubscriber;
  onMount(async () => {
    unsubscribe_from_refresh = timetable_list_should_be_refreshed.subscribe(
      async (should_be_refreshed) => {
        if (!should_be_refreshed) return;

        const timetable_request = await get_shared_timetables(
          $token_information.a,
        );
        if (timetable_request.isOk()) {
          availableTimetables = [...timetable_request.value];
        }

        timetable_list_should_be_refreshed.set(false);
      },
    );

    timetable_list_should_be_refreshed.set(true);
  });

  onDestroy(() => {
    unsubscribe_from_refresh();
  });
</script>

<p class="mt-2 text-xl font-semibold">Shared with me</p>
<hr class="my-2 h-px border-0 bg-gray-200" />

<TimetableList
  {availableTimetables}
  editing_allowed={false}
  deletion_allowed={false}
></TimetableList>

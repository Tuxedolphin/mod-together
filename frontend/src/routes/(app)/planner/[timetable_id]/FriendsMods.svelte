<script lang="ts">
  import {
    currentlySelectedMods,
    currentUserInformation,
  } from "$lib/shared/shared.svelte";
  import { onMount } from "svelte";
  import ModsSelectionComponent from "./ModsSelectionComponent.svelte";

  let friends_mods = $derived(
    $currentlySelectedMods.filter(
      (x) => x.profile.userId !== $currentUserInformation.userId,
    ),
  );

  onMount(() => {
    console.log(friends_mods);
  });

  interface ModsSelectionComponentProps {
    acadYear: string;
    semester: number;
    timetable_id: string | undefined;
  }

  onMount(() => {
    console.log("Friend:");
    console.log($state.snapshot(friends_mods));
  });
</script>

{#each friends_mods as mods}
  <ModsSelectionComponent
    is_friend={true}
    acadYear={mods.academicYear}
    semester={mods.semester}
    timetable_id={mods.id}
    timetable_name={mods.name}
    visibility={"publicEdit"}
  ></ModsSelectionComponent>
{/each}

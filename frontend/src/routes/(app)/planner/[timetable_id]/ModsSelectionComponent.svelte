<script lang="ts">
  import SearchBar from "$lib/components/SearchBar.svelte";
  import { onMount } from "svelte";
  import SelectedModuleList from "./SelectedModuleList.svelte";
  import {
    currentlySelectedMods,
    currentUserInformation,
  } from "$lib/shared/shared.svelte";
  import ImportFromNUSMods from "$lib/components/ImportFromNUSMods.svelte";
  import ImportFromNusModsButton from "$lib/components/Buttons/ImportFromNusModsButton.svelte";
  import AddFromOtherTimetablesButton from "$lib/components/Buttons/AddFromOtherTimetablesButton.svelte";
  import type { RoomVisibility } from "$lib/types/db_raw_types";

  interface ModsSelectionComponentProps {
    acadYear: string;
    semester: number;
    timetable_id: string | undefined;
    visibility: RoomVisibility;
    timetable_name: string;
    is_friend: boolean;
    user_favourite_color: string;
  }

  let {
    acadYear,
    semester,
    timetable_id,
    visibility,
    timetable_name,
    user_favourite_color,
    is_friend = false,
  }: ModsSelectionComponentProps = $props();

  let user_info = $derived(
    $currentlySelectedMods.find((x) => x.id === timetable_id),
  );
  let selected_user_mods_list = $derived(user_info?.metaData || []);

  // let is_own_mods_list = $derived;
</script>

{#if user_info}
  {#if is_friend}
    @{user_info.profile.handle}'s Mod List:
  {:else}
    Your Mods List:{/if}
  <SearchBar
    {user_favourite_color}
    {timetable_name}
    {acadYear}
    {semester}
    {timetable_id}
  ></SearchBar>
{:else}
  <div class="text-xl">Your Mods:</div>
  <SearchBar
    {user_favourite_color}
    {timetable_name}
    {acadYear}
    {semester}
    {timetable_id}
  ></SearchBar>
{/if}

{#if selected_user_mods_list.length !== 0}
  <SelectedModuleList {timetable_name} {acadYear} {semester} {timetable_id}
  ></SelectedModuleList>
{:else}
  <div>List is empty.</div>
  {#if !is_friend}
    <div class="flex text-center flex-col">
      <ImportFromNusModsButton
        user_favourite_colour={user_favourite_color}
        acad_year={acadYear}
        {semester}
        current_timetable_id={timetable_id}
        timetable_name={$currentlySelectedMods[0].name}
      ></ImportFromNusModsButton>
    </div>
    <div class="divider">OR</div>
    <div class="w-full">
      <AddFromOtherTimetablesButton
        acad_year={acadYear}
        current_timetable_id={timetable_id}
        timetable_name={$currentlySelectedMods[0].name}
        {semester}
      ></AddFromOtherTimetablesButton>
    </div>
  {/if}
{/if}

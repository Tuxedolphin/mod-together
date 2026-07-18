<script lang="ts">
  import NavItemLargeScreen from "./NavItemLargeScreen.svelte";

  import {
    Home,
    House,
    Menu,
    Settings,
    Settings2,
    Share,
    Share2,
    UserRound,
    type LucideIcon,
  } from "@lucide/svelte";

  import mods_tgt_header from "$lib/assets/mods_tgt_header.png?enhanced";
  import type { LayoutProps } from "../$types";
  import UserAvatar from "./UserAvatar.svelte";

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  let { children }: LayoutProps = $props();

  import { currentUserInformation } from "$lib/shared/shared.svelte";
  import { goto } from "$app/navigation";
  import { resolve } from "$app/paths";
  import type { NavigationItemProp } from "$lib/types/internal";

  const navigation_items: NavigationItemProp[] = [
    { icon: House, title: "Home", path: "/home" },
    { icon: Share2, title: "Shared with me", path: "/shared" },
    { icon: UserRound, title: "Profile", path: "/me" },
    { icon: Settings2, title: "Settings", path: "/settings" },
  ];
</script>

<div class="px-2 md:px-0 bg-base-200 shadow-sm">
  <div class="container flex justify-between mx-auto">
    <div class="flex items-center">
      <label for="main-drawer" aria-label="open sidebar">
        <!-- <Menu></Menu> -->
      </label>
      <enhanced:img
        onclick={() => {
          goto(resolve("/(app)/home"));
        }}
        class="aspect-5/2 h-14 w-auto align-middle"
        src={mods_tgt_header}
        alt="Mods Together Logo"
      />
    </div>
    <!-- <div class="flex items-center gap-4">
      <div class="flex gap-1">
        <Share2></Share2>
        <p>Shared</p>
      </div>
      <div class="flex gap-1">
        <House></House>
        <p>Home</p>
      </div>
      <div class="flex gap-1">
        <House></House>
        <p>Home</p>
      </div>
    </div> -->
    <div class="flex items-center gap-1">
      <p>@{$currentUserInformation.handle}</p>
      <UserAvatar></UserAvatar>
    </div>
  </div>
</div>

<div class="container mx-auto px-2 md:px-0">
  <div class="flex">
    <div class="flex flex-col gap-2 min-w-48 mt-4">
      {#each navigation_items as item}
        <NavItemLargeScreen information={item}></NavItemLargeScreen>
      {/each}
    </div>
    <div class="w-full">
      {@render children()}
    </div>
  </div>
</div>

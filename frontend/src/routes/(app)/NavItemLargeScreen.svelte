<script lang="ts">

  import type { NavigationItemProp } from "$lib/types/internal";
import { page } from '$app/state';
  import { goto } from "$app/navigation";
  import { resolve } from "$app/paths";
 
  interface NavigationLargeItemInfo {
    information: NavigationItemProp,
    top_bar: boolean
  }
  let {information}: NavigationLargeItemInfo = $props();
  let selected = $derived(page.url.pathname.includes(information.path));
</script>

<div class="flex items-center justify-center md:justify-normal gap-1 {selected ? "bg-base-300" : ""} rounded-4xl h-12 md:pl-4 md:mr-2 md:w-auto w-full" onclick={() => {
    goto(information.path)
}}>
  {const Icon = $derived(information.icon)}
  <Icon />
  <p class="hidden xl:block">{information.title}</p>
</div>

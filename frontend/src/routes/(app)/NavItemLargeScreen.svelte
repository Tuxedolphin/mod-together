<script lang="ts">

  import type { NavigationItemProp } from "$lib/types/internal";
import { page } from '$app/state';
  import { goto } from "$app/navigation";
  import { resolve } from "$app/paths";
 
  interface NavigationLargeItemInfo {
    information: NavigationItemProp,
  }
  let {information}: NavigationLargeItemInfo = $props();
  let selected = $derived(page.url.pathname.includes(information.path));
</script>

<div class="flex items-center gap-1 {selected ? "bg-base-300" : ""} rounded-4xl h-12 pl-4 mr-2" onclick={() => {
    goto(information.path)
}}>
  {const Icon = $derived(information.icon)}
  <Icon />
  <p>{information.title}</p>
</div>

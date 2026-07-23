<script lang="ts">
  import LoginComponent from "$lib/components/LoginPage/LoginComponent.svelte";
  import ModTogetherHero from "$lib/components/LoginPage/ModTogetherHero.svelte";
</script>

<ModTogetherHero>
  <div class="alert alert-error alert-soft text-center">
    <span>⚠️ </span>
    Mods Together is in Beta. Timetable information might be lost during certain
    version changes. Remember to always keep a safe copy with you. Happy Modding!
  </div>

  <div class="py-2">
    <div class="text-info-content bg-info p-2 rounded-2xl">
      You may report any issues <a
        class="underline"
        href="https://forms.gle/Aggf9kMYjaLchtrPA">here</a
      >
    </div>
  </div>
  <LoginComponent></LoginComponent>
  <!-- <div class="divider">OR</div> -->
  <!-- <GuestTryoutComponent></GuestTryoutComponent> -->
</ModTogetherHero>

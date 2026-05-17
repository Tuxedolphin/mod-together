<script lang="ts">
    const currentDay = new Date().getDay();
    // Get Localised Sunday to Sat:
    const daysOfWeek = [
        new Date("25 Jan 2026"), // Sunday
        new Date("26 Jan 2026"),
        new Date("27 Jan 2026"),
        new Date("28 Jan 2026"),
        new Date("29 Jan 2026"),
        new Date("30 Jan 2026"),
        new Date("31 Jan 2026"), // Monday
    ]

    const dateFormatter = new Intl.DateTimeFormat(undefined, {weekday: "short"})
    
    function formatShortDate(date: Date): string {
        return dateFormatter.format(date);
    }

    
</script>

<div>

    <div class="columns-5 text-center">
        {#each {length: 5}, i}
            <div>{formatShortDate(daysOfWeek[i+1])} </div>
        {/each}
    </div>
</div>
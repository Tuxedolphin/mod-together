import type { ModSummary } from '../types/mod_summaries';
import type { PageLoad } from './$types';

export const load = (async ({fetch}) => {
    const res = await fetch('https://api.nusmods.com/v2/2025-2026/moduleList.json')
    const json: ModSummary[] = await res.json();

    return {
        data: json
    };
}) satisfies PageLoad;